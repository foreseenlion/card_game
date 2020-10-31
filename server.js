// These two first lines create your server instance
var server = require('http').createServer();
var io = require('socket.io')(server);

// This defines the port that we'll be listening to
server.listen(3000);

io.sockets.on('connection', function(socket)
{
	console.log('User connected: ' + socket.id);
	socket.emit('connectionEstabilished', {id: socket.id});
	socket.broadcast.emit('connectionEstabilished', {id: socket.id});

	socket.on('move', function(data)
	{
		console.log(data.message);
		socket.broadcast.emit("foreignMessage", {id: socket.id, message: data.message});
	});
});
