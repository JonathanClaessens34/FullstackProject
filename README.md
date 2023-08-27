# Fullstack Project Team 11

## Team members

- Jonathan Claessens
- Steven Breemans
- Jonas Steveneerspxl
- Noah Schoeters

## Folder structure

- Readme.md
- _architecture_: this folder contains documentation regarding the architecture of your system.
- `docker-compose.yml` : to start the backend (starts all microservices)
- _backend-java_: contains microservices written in java
- _backend-net_: contains microservices written in C#
- _demo-artifacts_: contains images, files, etc that are useful for demo purposes.
- _frontend-web_: contains the Angular webclient
- _frontend-app_: contains the mobile client written in MAUI

Each folder contains its own specific `.gitignore` file.  
**:warning: complete these files asap, so you don't litter your repository with binary build artifacts!**

## How to setup and run this application

:heavy_check_mark:_(COMMENT) Add setup instructions and provide some direction to run the whole  application: frontend to backend._

### Angular frontend:
Go with your terminal to this folder and execute following cmd:

```docker-compose up```

After running this cmd, you can access the angular frontend at: 'http://localhost:3000/'

### Java backend:

In your terminal, enter and execute following cmd:

```docker run -d -p 9411:9411 openzipkin/zipkin```

Open backend-java folder in intellij and run following services:
- config-service
- discovery-service
- gateway-service
- messaging-service
- menu-service
- popupbar-service

Install and open XAMPP

Start following modules:
- Apache
- MySQL

In MySQL, create a db named 'fullstack_db'

In XAMPP you can do this by clicking on Admin button of Mysql and clicking on New on the upper left

### .NET backend:

Open backend-net\BackendDotNet\src\BackendDotNet.sln with visual studio and set docker-compose as startup project and run it

### MAUI frontend

Open frontend-app\Order.Mobile\Order.Mobile.sln with visual studio and run Windows Machine or another emulator