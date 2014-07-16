app.factory('MyChatService', function ($resource) {
    var requestUri = 'http://localhost:1053/';

    return $resource(requestUri, {},
        {
            "post": { method: 'POST', url: "http://localhost:1053/api/Session" },

        });

});