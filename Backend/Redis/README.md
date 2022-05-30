# Redis database 
If you already have a Docker and Redis setup you can skip the first few steps. 

#### Database setup
This guide will help you install the database through docker. 

1. Download and install [docker](https://www.docker.com/get-started/)
2. Open a terminal in this folder, and run the command: 
`docker-compose up -d` from the command line. 
3. To enter the primary docker container, run `docker exec -it redis_redis-primary_1 bash`
*There is also a replica called redis_redis-secondary_1*
4. In the Redis container, type `redis-cli -a my_password` to enter the command line interface
5. You are ready to run the program. 
