app.controller('SessionController', function ($resource, MyChatService) {

    var currentdate = new Date();
    this.SessionId = guid();
    this.Owner;
    this.Topic;
    this.StartDateTime = currentdate;

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

    function guid() {
        function _p8(s) {
            var p = (Math.random().toString(16) + "000000000").substr(2, 8);
            return s ? "-" + p.substr(0, 4) + "-" + p.substr(4, 4) : p;
        }
        return _p8() + _p8(true) + _p8(true) + _p8();
    }

});