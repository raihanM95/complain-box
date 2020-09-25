/* email validation */
function myFunction() {
    
    validateEmail($('#email').val());
    return false;
};

function validateEmail(email) {
    var re = /^\s*[\w\-\+_]+(\.[\w\-\+_]+)*\@[\w\-\+_]+\.[\w\-\+_]+(\.[\w\-\+_]+)*\s*$/;
    if (re.test(email)) {
        if (email.indexOf('@diu.edu.bd', email.length - '@diu.edu.bd'.length) !== -1) {
            //alert('');
        } else {
            alert('Not a valid e-mail address!');
            //return;
        }
    } else {
        alert('Not a valid e-mail address!');
        //return;
    }
}
/* //email validation */