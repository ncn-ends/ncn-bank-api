<img src="https://github.com/ncn-ends/ncn-bank-api/blob/master/bank-tech-banner.jpeg?raw=true" alt="Banner for repo">

# ncn-bank-api

## Introduction

A mock banking system built with .NET, Dapper, and Postgres  
If interested you can view my unstructured notes as I built the project [here](./NOTES.md) and [here](./REQUIREMENTS.md).

## Goals

- See how productive / efficient I can be when building out an API with .NET by measuring the amount of time it takes me
- Sharpen my skillset with SQL and the .NET ecosystem
- Build the project from the ground up with TDD

## Results

> See how productive / efficient I can be when building out an API with .NET by measuring the amount of time it takes me
- Was able to complete the project in 3-4 weeks
- Felt really productive with TDD

> Sharpen my skillset with SQL and the .NET ecosystem
- Explored minimal API
- Explored Dapper
- Got better at using SQL and explored some new techniques and concepts such as event sourcing

> Build the project from the ground up with TDD
- First time building a project from the ground up with TDD
- Felt really productive to use TDD instead of manual testing with Postman
- Changing core components doesn't feel as daunting because you don't have to worry about breaking changes
- TDD gets faster the longer you do it
- IDE tools make TDD feel faster and smoother


<img src="https://github.com/ncn-ends/ncn-bank-api/blob/master/ncn-bank-api-schema-graph.png?raw=true" alt="Schema graph">

## Usage

1) Clone the repo
2) Run the script at root `start-local-db.sh`
    - You will need `docker` and `docker-compose` installed
3) Set the connection string
    - If in development, set the User Secrets like so:
```
"ConnectionStrings": {
    "Default": "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=postgres;Include Error Detail=true;"
}
```
   - These settings reflect the settings defined in the `docker-compose.yml`
     - If you change the docker-compose file, you will need to change the connection string to reflect the changes
   - If in production, set the same connection string as an environment variable called `POSTGRESQLCONNSTR_Default`
4) Run the application in `Api`

## Authors

- [@ncn-ends](https://www.github.com/ncn-ends)


## License

[MIT](https://choosealicense.com/licenses/mit/)