# CoreAngular
- OS: Linux
- Docker hub: https://hub.docker.com/r/lamkhai/coreangular
- Docker client:
  - Build:
	```
	docker build -t lamkhai/coreangular .
	```
  - Run:
	- Development (Without using compose):
	  - Create network:
		```
	    docker network create coreangular-network-dev
	    ```
	  - Run via Container (Dockerfile) or via the following command:
	    ```
	    docker run --name=LK-CoreAngular-Cli -p 8080 --network=coreangular-network-dev -e ASPNETCORE_ENVIRONMENT=Development lamkhai/coreangular
	    ``` 