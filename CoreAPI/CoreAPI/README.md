# CoreAPI
- OS: Linux
- Docker hub: https://hub.docker.com/r/lamkhai/coreapi
- Docker client:
  - Build:
	```
	docker build -t lamkhai/coreapi .
	```
  - Run:
	- Without compose:
	  - Create network:
		```
	    docker network create coreapi-network-dev
	    ```
	  - Run Postgres DB:
	    ```
	    docker run --name=LK-CoreAPI-DB-Dev -p 5433:5432 -d --network coreapi-network-dev --network-alias coreapi-postgres-dev -v coreapi-db-data-dev:/var/lib/postgresql/data -e POSTGRES_DB=CoreAPIDB -e POSTGRES_USER=lamkhai -e POSTGRES_PASSWORD=@Password postgres:11.4
	    ```
	  - Run via Container (Dockerfile) or via the following command:
		```
		docker run --name=LK-CoreAPI-Cli -p 52915:8080 --network=coreapi-network-dev -e ASPNETCORE_ENVIRONMENT=Development lamkhai/coreapi
		``` 
	- With compose: