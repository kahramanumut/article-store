# Article Store Microservices

- Article API
- Review API
- Gateway API (Ocelot)

### Instructions

* Run 'docker-compose up'
* You can use Swagger or Postman with Gateway (http://localhost:5003) 
* I've uploaded Postman collection for Gateway, you don't need to call seperate for each API
* Article datas seed in own DB, but Review API needs new datas. 

##### Tech Stack & Libraries

* .Net 5
* CQRS
* Mediatr
* Ocelot Gateway
* OData
* DDD
* EF Core
* Postgresql
* FluentValidation
* Swagger
* Docker

##### Scheme
![Scheme](https://github.com/kahramanumut/article-store/blob/main/_images/article-store-architect.png?raw=true)

 * Communication of between APIs provide with HTTP Calls
 * When a new review adding, Review API checks either there is any article via Article OData service
 * At the same time, when a article deleting, Article API checks either there is any review via Review OData service

PS : If you don't have any idea about OData, you can take a look my OData article
https://medium.com/@kahramanumut/4-satır-kod-ile-net-coreda-odata-web-api-oluşturma-dca5d83f1e54