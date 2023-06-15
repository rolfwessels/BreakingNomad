using System.Text.Json;
using BreakingNomad.Api.Helper;

namespace BreakingNomad.Api.Data;

public class JsonDataStore<TDoModel>
{
  private static readonly Semaphore _mutex = new Semaphore(1,1);
  private readonly Action<string, TDoModel> _addId;
  private readonly Func<TDoModel, string> _getId;
  private readonly string _folder;
  private readonly string _file;

  public JsonDataStore(string folder, Action<string, TDoModel> addId, Func<TDoModel,string> getId)
  {
    _folder = folder;
    _addId = addId;
    _getId = getId;
    _file = Path.Combine(_folder, $"{typeof(TDoModel).Name}.json");
  }

  public  Task<TDoModel> Add(TDoModel request)
  {
   return Wrap (async () => {
     var readJsonFile =await ReadJsonFile();
     _addId(Guid.NewGuid().ToString("n"),request);
     readJsonFile.Add(request);
     await WriteToFile(readJsonFile);
     return request;
    });
    
  }

  public Task<TDoModel> Update(string id, TDoModel request)
  {
    return Wrap (async () => {
      var readJsonFile =await ReadJsonFile();
      var found = readJsonFile.FirstOrDefault(x=>_getId(x) == id);
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
      var remove = readJsonFile.RemoveAll(x =>_getId(x) == requestId);
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
    return JsonSerializer.Deserialize<List<TDoModel>>(readAllTextAsync,JsonSerializeHelper.DefaultIndented)!;
  }
}
