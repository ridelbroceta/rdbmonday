var Tools;
(function (Tools) {
    var BlockUi = (function () {
        function BlockUi() {
            var msg = '<h4><i class="fa fa-refresh fa-spin"></i> &nbsp;Loading...</h4>';
            var messageWidth = "200";
            var messageHeight = "70";
            $.blockUI({
                // message displayed when blocking (use null for no message) 
                message: msg,
                // styles for the message when blocking; if you wish to disable 
                // these and use an external stylesheet then do this in your code: 
                // $.blockUI.defaults.css = {}; 
                css: {
                    width: messageWidth + "px",
                    height: messageHeight + "px",
                    top: '50%',
                    left: '50%',
                    margin: (-messageHeight / 2) + 'px 0 0 ' + (-messageWidth / 2) + 'px',
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .8,
                    color: '#fff',
                },
                // set these to true to have the message automatically centered 
                centerX: false,
                centerY: false,
                // style to replace wait cursor before unblocking to correct issue 
                // of lingering wait cursor 
                cursorReset: 'default',
                // z-index for the blocking overlay 
                baseZ: 2000,
                // if it is already blocked, then ignore it (don't unblock and reblock) 
                ignoreIfBlocked: false
            });
        }
        return BlockUi;
    }());
    Tools.BlockUi = BlockUi;
})(Tools || (Tools = {}));
//# sourceMappingURL=my-blockUI.js.map