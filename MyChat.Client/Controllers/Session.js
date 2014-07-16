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


        try {

            var SessionDto = { SessionId: this.SessionId, Owner: this.Owner, Topic: this.Topic, StartDateTime: this.StartDateTime };

            var data = MyChatService.post(SessionDto,
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

    this.AddParticipant = function (name, email) {
        var name1 = name;
        var email1 = email;
        var participant = { Name: name1, Email: email1 };
        try
        {
            this.participants.push(participant);
            this.name = "";
            this.email = "";
        }
        catch(ex)
        {

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