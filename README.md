[![Github release](https://img.shields.io/github/v/release/rolfwessels/BreakingNomad)](https://github.com/rolfwessels/BreakingNomad/releases)
[![Dockerhub Status](https://img.shields.io/badge/dockerhub-ok-blue.svg)](https://hub.docker.com/r/rolfwessels/BreakingNomad/tags)
[![Dockerhub Version](https://img.shields.io/docker/v/rolfwessels/BreakingNomad?sort=semver)](https://hub.docker.com/r/rolfwessels/BreakingNomad/tags)
[![GitHub](https://img.shields.io/github/license/rolfwessels/BreakingNomad)](https://github.com/rolfwessels/BreakingNomad/licence.md)

![breaking nomad](./docs/logo.png "breaking nomad")

# Breaking nomad

This makes breaking nomad happen

## Getting started

Open the docker environment to do all development and deployment

```bash
# bring up dev environment
make build up
# build the project ready for publish
make publish
```

## Available make commands

### Commands outside the container

- `make up` : brings up the container & attach to the default container
- `make down` : stops the container
- `make build` : builds the container
- `docker-login` : Log into docker
- `make build` : builds the container
- `make build` : builds the container

### Commands to run inside the container

- `make start` : Run the breaking nomad
- `make publish` : Build the breaking nomad to build folder
- `make deploy` : Deploy the breaking nomad

## Research

- <https://opensource.com/article/18/8/what-how-makefile> What is a Makefile and how does it work?
