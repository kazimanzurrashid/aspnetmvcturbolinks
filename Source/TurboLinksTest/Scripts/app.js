/* jshint browser: true */
/* global jQuery: false, Turbolinks: false  */

(function ($, Turbolinks) {
    'use strict';
    
    window.visitRoot = function() {
        Turbolinks.visit('/');
    };

    window.visitProject = function(id) {
        var url = '/projects/details/' + id;
        Turbolinks.visit(url);
    };

    $(document).on('page:load', function () {
        $('form').each(function () {
            $.data(this, 'validator', void (0));
        });
        $.validator.unobtrusive.parse('form');
    });
})(jQuery, Turbolinks);