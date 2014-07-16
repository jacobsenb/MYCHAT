app.factory('MyChatService', function ($resource) {
    var requestUri = 'http://localhost:1053/';

    return $resource(requestUri, {},
        {
            "getSession": { method: 'GET', params: {guid:null}, url: "http://localhost:1053/api/Session/{guid}" },
            "postSession": { method: 'POST', url: "http://localhost:1053/api/Session" },

            "getPractice": { method: 'GET', params: { guid: null }, url: "http://localhost:1053/api/Practice/{guid}" },
            "postPractice": { method: 'POST', url: "http://localhost:1053/api/Practice" },

            "getClient": { method: 'GET', params: { practiceId: null }, url: "http://localhost:1053/api/Practice/{practiceId}/Clients" },
            "postClient": { method: 'POST', params: { practiceId:null }, url: "http://localhost:1053/api/Practice/{practiceId}/Clients" },

            "getParticipant": { method: 'GET', params: { sessionId: null }, url: "http://localhost:1053/api/{sessionId}/Participants" },
            "postParticipant": { method: 'POST', params: { sessionId: null }, url: "http://localhost:1053/api/{sessionId}/Participants" }
        });
});