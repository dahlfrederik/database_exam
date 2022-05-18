# PostgreSQL database 
If you already have a postgreSQL setup you can skip the first few steps. 

#### Database setup
This guide will help you install the database through docker. 

1. Download and install [docker](https://www.docker.com/get-started/)
2. Download the PostgreSQL docker [image](https://hub.docker.com/_/postgres) by running the command: 
`docker pull postgres` from the command line. 
3. Download a PostgreSQL client, we recommend [DBeaver](https://dbeaver.com/download/) Community Edition. Open the program and run initial setup.
4. Create a connection to a new PostgreSQL database. The docker image is running on `port 5555`. Password should be set to: `hemmelighed17`
5. Create a new database called `users` and set it as default. 
6. Open the database script: `create_postgres_db.sql` and execute it. 
7. The database should now be populated and up to date. Verify it and proceed to next step. 
8. You are ready to run the program. 


