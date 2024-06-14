# Microservices

## CoreAPI
- OS: Linux
- Docker hub: https://hub.docker.com/r/lamkhai/coreapi
- Docker client:
  - Build:
	```
	docker build -t lamkhai/coreapi .
	```
  - Run:
	```
	docker run -p 52915:8080 -p 52916:8081 --name=LK-CoreAPI lamkhai/coreapi
	```

## GettingStartedApp
- OS: Linux
- Docker hub: https://hub.docker.com/r/lamkhai/gettingstartedapp
- Docker client:
  - Build image:
	```
	docker build -t lamkhai/gettingstartedapp .
	```
  - Run with creating a volume:
	```
	docker run -dp 127.0.0.1:3000:3000 --name=LK-GettingStarted-Volume --mount type=volume,src=todo-db,target=/etc/todos lamkhai/gettingstartedapp
	```
  - Run with creating a bind:
	```
	docker run -it --name=LK-GettingStarted-Bind --mount type=bind,src="$(pwd)",target=/src/gettingstartedapp ubuntu bash
	```
  - Run with creating a bind, in a development container:
	```
	docker run -dp 127.0.0.1:3000:3000 --name=LK-GettingStarted-Bind-Dev `
	-w /app --mount "type=bind,src=$pwd,target=/app" `
	node:18-alpine `
	sh -c "yarn install && yarn run dev"
	```
  - Add MySQL container:
	- Create the network:
	  ```
	  docker network create gettingstarted-network
	  ```
	- Start a MySQL container and attach it to the network:
	  ```
	  docker run -d --name=LK-GettingStarted-MySQL `
  	  --network gettingstarted-network --network-alias gettingstarted-network-mysql `
  	  -v gettingstarted-mysql:/var/lib/mysql `
  	  -e MYSQL_ROOT_PASSWORD=gettingstarted-password `
  	  -e MYSQL_DATABASE=gettingstarted-db `
  	  mysql:8.0
	  ```
	- Start a new container using the nicolaka/netshoot image:
	  ```
	  docker run -it --name=LK-GettingStarted-Netshoot --network gettingstarted-network nicolaka/netshoot
	  ```
	  - Look up the IP of gettingstarted-network-mysql:
		```
		dig gettingstarted-network-mysql
		```
  - Run with MySQL container:
	```
	docker run -dp 127.0.0.1:3000:3000 --name=LK-GettingStarted-LKTODO `
	-w /app -v "$(pwd):/app" `
	--network gettingstarted-network `
	-e MYSQL_HOST=gettingstarted-network-mysql `
	-e MYSQL_USER=root `
	-e MYSQL_PASSWORD=gettingstarted-password `
	-e MYSQL_DB=gettingstarted-db `
	node:18-alpine `
	sh -c "yarn install && yarn run dev"
	```
  - Use a Compose file: GettingStartedApp\compose.yaml
	- Run:
	  ```
	  docker compose up -d
	  ```
	- Tear it all down:
	  ```
	  docker compose down
	  ```