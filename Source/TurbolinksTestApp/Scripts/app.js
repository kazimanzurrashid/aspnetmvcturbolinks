/* jshint browser: true */
/* global jQuery: false, Turbolinks: false  */

(function ($, Turbolinks, globals) {
    'use strict';
    
    globals.visitRoot = function () {
        Turbolinks.visit('/');
    };

    globals.refreshCurrent = function () {
        Turbolinks.visit(globals.location.pathname);
    };

    $(document).on('page:load', function () {
        $('form').each(function () {
            $.data(this, 'validator', void (0));
        });
        $.validator.unobtrusive.parse('form');
    });

    $(document).on('click', '.task-list input[type="checkbox"]', function () {
        var form = $(this).closest('form'),
          options = {
              url: form.attr('action'),
              method: form.attr('method'),
              data: form.serializeArray(),
              success: globals.refreshCurrent
          };

        options.data.push({ name: "X-Requested-With", value: "XMLHttpRequest" });

        $.ajax(options);
    });
})(jQuery, Turbolinks, window);