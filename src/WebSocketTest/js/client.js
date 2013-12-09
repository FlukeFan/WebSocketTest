
(function () {

    $(document).on('click', '#start', start);

    function start() {
        $('#start').attr('disabled', true);
        write('start');

        var url = 'ws://localhost:8090/';
        var _server = WebSocket(url);

        _server.onopen = function () {
            Try(function () {
                write('onopen');
                setTimeout(send, 500);
            });
        };

        _server.onmessage = function (data) {
            data = data.data || data;
            write(data);

            if (data === 'RECEIVED')
                $('#done').text('Complete');
        };

        function send() {
            Try(function () {
                write('send');
                _server.send('OPENED');
            });
        }
    }

    function Try(action) {
        try {
            action();
        } catch (e) {
            write('ERROR: ' + e);
        }
    }

    function write(msg) {
        $('<li>' + msg + '</li>').prependTo('#output');
    }

}());

