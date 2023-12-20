//When the user click on the link and hide,scroll to the document

$(function () {
	var navMain = $(".navbar-collapse");

	navMain.on("click", "a", null, function () {
		navMain.collapse('hide');
	});
});


// header fixed on scroll

$(window).on("scroll", function () {
	if ($(window).scrollTop() > 50) {
		$("nav").addClass("active");
	} else {
		//remove the background property so it comes transparent again (defined in your css)
		$("nav").removeClass("active");
	}
});


//Get the button
var mybutton = document.getElementById("myBtn");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () {
	scrollFunction()
};

function scrollFunction() {
	if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
		mybutton.style.display = "block";
	} else {
		mybutton.style.display = "none";
	}
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
	document.body.scrollTop = 0;
	document.documentElement.scrollTop = 0;
}



jQuery(document).ready(function ($) {
    "use strict";
    $('#customers-testimonials').owlCarousel({
        loop: true,
        center: true,
        margin: 30,
        autoplay: true,
        dots: true,
        nav: true,
        autoplayTimeout: 8500,
        smartSpeed: 450,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        responsive: {
            0: {
                items: 1
            },
            1024: {
                items: 2.2
            },
            1920: {
                items: 3.5
            }
        }
    });
});