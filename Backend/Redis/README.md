# Redis database 
If you already have a Docker and Redis setup you can skip the first few steps. 

#### Database setup
This guide will help you install the database through docker. 

1. Download and install [docker](https://www.docker.com/get-started/)
2. Download the Redis docker [image](https://hub.docker.com/_/redis) by running the command: 
`docker run --name redis -p 6379:6379 -d redis` from the command line. 
3. To enter the docker container, run `docker exec -it redis bash`
4. In the Redis container, type `redis-cli` to enter the command line interface
5. You are ready to run the program. 
