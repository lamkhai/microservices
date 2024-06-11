# Microservices

## CoreAPI
- OS: Windows
- Docker hub: https://hub.docker.com/r/lamkhai/coreapi
- Docker client:
  - Build:
	```
	docker build -t lamkhai/coreapi .
	```
  - Run:
	```
	docker run -p 52915:8080 -p 52916:8081 --name=LamKhaiCoreAPI lamkhai/coreapi
	```

## GettingStartedApp
- OS: Linux
- Docker hub: https://hub.docker.com/r/lamkhai/gettingstartedapp
- Docker client:
  - Build:
	```
	docker build -t lamkhai/gettingstartedapp .
	```
  - Run with creating a volume:
	```
	docker run -dp 127.0.0.1:3000:3000 --name=LamKhaiGettingStartedApp --mount type=volume,src=todo-db,target=/etc/todos lamkhai/gettingstartedapp
	```