# ![img_task](https://i.imgur.com/yYs8S9A.png) **BackendTest**
Hello everyone!

This is the recruitment task I have done to confirm my skills while working with Web API (.NET Core technology).
I have solved the following problems here

- I implemented simple unit tests
- I implemented basic crud operations
- Includes implemented simple authentication
- The application is made in a three-layer architecture (Data access layer, Business Logic Layer, API)
- Automatic migration execution during the need to update the database has been implemented
- Implements automatic creation of documentation that can be used by the recruiter for api testing

## Done
- [x] Create Company.
- [x] Search Company.
- [x] Update Company.
- [x] Delete Company.
- [x] Basic Authentication.
- [x] Use Entity Framework / NHibernate.

### Authentication
while doing the authentication, I used a ready-made solution, which is located below
- [ZNetCS.AspNetCore.Authentication.Basic](https://github.com/msmolka/ZNetCS.AspNetCore.Authentication.Basic)

## API
**API Url**: *http://localhost:5000*  
**Login**: *admin*  
**Password**: *admin*

**Endpoints**
![img_endpoints](https://imgur.com/5vscbbr.png)


## Setup
#### Requirements
- [.NET Core](https://dotnet.microsoft.com/download), *>= 3.1*
- MariaDB

#### Install MySQL
When installing the database you can use this docker file (_docker-compose.yaml_)
 ```yaml
version: "3.7"

services:
  mariadb:
    image: mariadb
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: root
    volumes:
      - ./data:/var/lib/mysql
    ports:
      - 3306:3306
```
and call in the console 
````bash
docker-compose up
````

#### Installation of packages 
```bash
dotnet restore
```
#### Do the tests
```bash
dotnet test
```

#### Run development server
By default, the server starts on port 5001 (http: 5000). **Before starting the server, you must create a database named** `pumox_test`

```bash
dotnet run
```

It's only a recruitment task, it performs only simple crud tasks!
