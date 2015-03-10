$('input:checkbox').change(function (e) {
    e.preventDefault();
    if ($('input[type=checkbox]:checked').length > parseInt($('#count').text())) {
        $(this).prop('checked', false);
        alert("You cannot vote for more Candidates");
    } else {
        $(this).closest('section').toggleClass('alert-danger alert-success');
    }
    if ($('input[type=checkbox]:checked').length == parseInt($('#count').text())) {
        $('#submit').removeAttr('disabled');
    } else {
        $('#submit').attr('disabled', 'disabled');
    }
});

//$('input[type=checkbox]').on('change', function () {
//    if ($('input[type=checkbox]:checked').length > parseInt($('#count').text())) {
//        $(this).prop('checked', false);
//    }
//    if ($('input[type=checkbox]:checked').length == parseInt($('#count').text())) {
//        $('#submit').show();
//    }
//});