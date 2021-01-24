﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {

    if ($("div.alert.notification")) {

        setTimeout(() => {
            $("div.alert.notification").fadeOut();
        }, 2000);
    }
    if ($("a.confirmDeletion")) {
        $("a.confirmDeletion").click(() => {
            if (!confirm("Confirm Deletion")) {
                return false;
            }
        });
    }

})
