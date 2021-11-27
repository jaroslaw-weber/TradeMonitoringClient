# Trade Monitoring - Client

Connect to server monitoring trades of securities (server-side) and displays data as a website. When connected by websocket to server, the server will push update every second.


### How to run

```
dotnet run watch
```

### How to run second instance
You can run multiple instances on same computer (need to switch ports).
Open second terminal and run this command:
```
dotnet run watch --urls=http://localhost:5001/
```
### How to run tests
```
dotnet test
```

### TODO
Things I did not have time to implement but could use improvement:
- unit tests