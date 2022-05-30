# Neo4j database 

#### Database setup

1. Download and install neo4j
2. Create a new project, and a new DBMS (names do not matter)
   1. Use the password "wwwdk123"
   2. user should be "neo4j" by default
3. Open Neo4j Browser
4. Run command ":play start", and click *Open Guide* under *Try Neo4j with live data*
5. Follow the guide for the first 4 steps, and run each command to set up the database
6. Create index for Movie Title:
   1. `CREATE INDEX indexMovTitle IF NOT EXISTS FOR (n:Movie) ON (n.title)`
