Lobibox.base.OPTIONS = $.extend({}, Lobibox.base.OPTIONS, {
    buttons: {
        ok: {
            text: '确定',
            closeOnClick: true
        },
        cancel: {
            text: '取消',
            closeOnClick: true
        },
        yes: {
            text: '是',
            closeOnClick: true
        },
        no: {
            text: '否',
            closeOnClick: true
        }
    }
});


function MessageBox() {
    
}

MessageBox.prototype.error = function(message) {
    Lobibox.alert('error', { msg: message, title: '错误'});
};

MessageBox.prototype.alert = function (message) {
    Lobibox.alert('info', { msg: message, title: '提示' });
};

MessageBox.prototype.success = function (message) {
    Lobibox.alert('success', { msg: message, title: '成功' });
};

MessageBox.prototype.warning = function (message) {
    Lobibox.alert('warning', { msg: message, title: '警告' });
};

MessageBox.prototype.confirm = function (message, ok, cancel) {
    Lobibox.confirm({
        msg: message, title: '请确认',
        callback: function ($this, type, ev) {
            var fun = type === 'yes' ? ok : cancel;
            fun && fun();
        }
    });
};

window.MessageBox = new MessageBox();