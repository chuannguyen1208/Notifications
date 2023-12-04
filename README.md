# .NET distributed services reverse proxy with Yarp

| Feature         | Status           | Desc |
| --------------- | ---------------- | --- |
| Auth / Host            | In progress      | localhost:5000 |
| Blog            | In Progress      | localhost:5001 |
| Notification    | Not Started      | --- |

## docker-compose-demo.yml
	- docker-compose -f docker-compose-demo.yml build
	- docker-compose -f docker-compose-demo.yml -p notification-demo up -d
