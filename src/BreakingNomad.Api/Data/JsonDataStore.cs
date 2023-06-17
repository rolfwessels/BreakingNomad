using System.Text.Json;
using BreakingNomad.Api.Helper;
using BreakingNomad.Shared.Services;
using Bumbershoot.Utilities.Helpers;

namespace BreakingNomad.Api.Data;

public class JsonDataStore<TDoModel> where TDoModel:IWithId
{
  private static readonly Semaphore _mutex = new Semaphore(1,1);
  private readonly string _folder;
  private readonly string _file;

  public JsonDataStore(string folder)
  {
    _folder = folder;
    _file = Path.Combine(_folder, $"{typeof(TDoModel).Name}.json");
  }

  public  Task<TDoModel> Add(TDoModel request)
  {
   return Wrap (async () => {
     var all =await ReadJsonFile();
     all.Add(request);
     await WriteToFile(all);
     return request;
    });
    
  }

  public Task<TDoModel> Update(string id, TDoModel request)
  {
    return Wrap (async () => {
      var readJsonFile =await ReadJsonFile();
      var found = readJsonFile.FirstOrDefault(x=>x.Id == id);
      if (found == null) throw new Exception("Not Found");
      readJsonFile.Remove(found);
      readJsonFile.Add(request);
      await WriteToFile(readJsonFile);
      return request;
    });
  }

  public async Task<List<TDoModel>> GetAll()
  {
    return await Wrap(ReadJsonFile);
  }

  public async Task<bool> Remove(string requestId)
  {
    return await Wrap(async () =>
    {
      if (string.IsNullOrEmpty(requestId)) throw new Exception("Id is required");
      var readJsonFile = await ReadJsonFile();
      var remove = readJsonFile.RemoveAll(x =>x.Id == requestId);
      await WriteToFile(readJsonFile);
      return remove > 0;
    });
  }

  private Task<T> Wrap<T>(Func<Task<T>> func)
  {
    try
    {
      _mutex.WaitOne();
      return Task.FromResult(func().Result);
    }
    finally
    {
      //Console.WriteLine("Exit: " + Thread.CurrentThread.ManagedThreadId + " is Completed its task");
      _mutex.Release();  
    }
  }

  private async Task WriteToFile(List<TDoModel> readJsonFile)
  {
    if (!Directory.Exists(_folder))
    {
      Directory.CreateDirectory(_folder);
    }
    var serialize = JsonSerializer.Serialize(readJsonFile,JsonSerializeHelper.DefaultIndented);
    await File.WriteAllTextAsync(_file, serialize);
  }

  private async Task<List<TDoModel>> ReadJsonFile()
  {
    if (!File.Exists(_file))
    {
      return new List<TDoModel>();
    }
    var readAllTextAsync = await File.ReadAllTextAsync(_file);
    return JsonSerializer.Deserialize<List<TDoModel>>(readAllTextAsync,JsonSerializeHelper.Default)!;
  }
}
