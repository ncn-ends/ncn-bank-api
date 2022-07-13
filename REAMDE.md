# ncn-bank-api

## Setup

### Development
1) Run `docker-compose -d docker-compose.yml` to start the Postgres database
2) Set the User Secrets for the `Api` Project like so:
```
{
  "ConnectionStrings": {
    "Default": "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=postgres;"
  }
}
```
- This should reflect the connection string for the local Docker Postgres database
3) `dotnet run` in the `/Api` directory

### Deployment
1) Edit `azure-deploy.sh` to replace the bash variables with the correct values

2) Setup a database cloud server using whichever provider you want.
   - Most of the database code should be database agnostic, but prefer Postgres
   - I chose [Supabase](https://supabase.com/)

3) Edit `example.azure-setup.sh` to replace the bash variables with the correct values and then rename the file to `azure-setup.sh`
    - This will be included in the `.gitignore`
    - No need to rename the deploy script since there's no sensitive information in that file

4) Run the `azure-deploy.sh` and then the `azure-setup.sh`
5) Navigate to the azure dashboard to set up CI/CD for the project using your own forked/cloned repo.
