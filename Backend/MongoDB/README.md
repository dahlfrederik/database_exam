# MongoDB database 

#### Database setup

1. Create a docker network run `docker network create mongoCluster`
2. Start MongoDB instances run the following commands <br />
`docker run -d --rm -p 27017:27017 --name mongo1 --network mongoCluster mongo:5 mongod --replSet myReplicaSet --bind_ip localhost,mongo1`<br /><br />
`docker run -d --rm -p 27018:27017 --name mongo2 --network mongoCluster mongo:5 mongod --replSet myReplicaSet --bind_ip localhost,mongo2`<br /><br />
`docker run -d --rm -p 27019:27017 --name mongo3 --network mongoCluster mongo:5 mongod --replSet myReplicaSet --bind_ip localhost,mongo3`<br />
3. Initiate the replica set run the following command <br />
`docker exec -it mongo1 mongosh --eval "rs.initiate({
 _id: \"myReplicaSet\",
 members: [
   {_id: 0, host: \"mongo1\"},
   {_id: 1, host: \"mongo2\"},
   {_id: 2, host: \"mongo3\"}
 ]
})"`

4. Test and verify the replica set run the following command <br />
`docker exec -it mongo1 mongosh --eval "rs.status()"`

5. You can now connect via the MongoDB Compass with localhost using port 27017 or use the terminal
6. Create index for Movie Ids:
    1. Run command `db.reviews.createIndex({"movieId": 1})`
