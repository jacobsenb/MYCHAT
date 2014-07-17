app.controller('SessionController', function ($resource, MyChatService) {

    var currentdate = new Date();
    this.SessionId = guid();
    this.Owner;
    this.Topic;
    this.email;
    this.name;
    this.practice;
    this.client;
    this.StartDateTime = currentdate;
    this.participants = new Array();
    //this.clients = {};

    this.CreateSession = function () {

        var practiceId = CreatePractice(this.practice);

        var clientId = CreateClient(practiceId, this.participants, this.SessionId);

        try {

            var SessionDto = { SessionId: this.SessionId, Owner: this.Owner, Topic: this.Topic, StartDateTime: this.StartDateTime };

            var data = MyChatService.postSession(SessionDto,
                function (successResult) {
                    // do something on success
                    console.log(successResult);
                }, function (errorResult) {
                    // do something on error
                    console.log(errorResult);
                });

        }
        catch (err) {

        }
    }

    function CreatePractice(practiceName) {
        var id = guid();

        try {

            var practiceDto = { PracticeId: id, Name: practiceName }

            var data = MyChatService.postPractice(practiceDto,
                   function (successResult) {
                       // do something on success
                       console.log(successResult);
                   }, function (errorResult) {
                       // do something on error
                       console.log(errorResult);
                   });

        }
        catch (err) {

        }

        return id;
    };

    function CreateClient(practiceId, participants,sessionId )
    {

        try {

            var arrayLength = participants.length;
            for (var i = 0; i < arrayLength; i++) {

                var id = guid();

                var clientDto = { ClientId: id, Name: participants[i].Name, Email: participants[i].Email, PracticeId: practiceId }

                var data = MyChatService.postClient({ practiceId: practiceId }, clientDto,
                       function (successResult) {

                           var id2 = guid();
                           // do something on success
                           var participantDto = { ParticipantId: id2, ClientId: id, Accepted: false, SessionId: sessionId }

                           var data = MyChatService.postParticipant({ sessionId: sessionId }, participantDto,
                                  function (successResult) {
                                      // do something on success
                                      console.log(successResult);
                                  }, function (errorResult) {
                                      // do something on error
                                      console.log(errorResult);
                                  });
                           console.log(successResult);
                       }, function (errorResult) {
                           // do something on error
                           console.log(errorResult);
                       });


            }
        }
        catch (err) {

        }
    }

    this.AddParticipant = function (name, email) {
        var name1 = name;
        var email1 = email;
        var participant = { Name: name1, Email: email1 };
        try {
            this.participants.push(participant);
            this.name = "";
            this.email = "";
        }
        catch (ex) {

        }

    };

    function guid() {
        function _p8(s) {
            var p = (Math.random().toString(16) + "000000000").substr(2, 8);
            return s ? "-" + p.substr(0, 4) + "-" + p.substr(4, 4) : p;
        }
        return _p8() + _p8(true) + _p8(true) + _p8();
    }

});