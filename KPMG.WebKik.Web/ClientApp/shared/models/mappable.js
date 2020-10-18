"use strict";
var Mappable = (function () {
    function Mappable() {
    }
    Mappable.prototype.map = function (serverObject) {
        var result = {};
        Object.assign(result, serverObject);
        return result;
    };
    return Mappable;
}());
exports.Mappable = Mappable;
//# sourceMappingURL=mappable.js.map