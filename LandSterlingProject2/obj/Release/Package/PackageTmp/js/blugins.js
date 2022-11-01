$(document).ready(function () {
  if (window.innerWidth > 991) {
    $(".landing .text").css({
      top: " 150px",
    });
  } else {
    $(".landing .text").css({
      top: " 100px",
    });
  }

  $(".landing-card ").css({
    left: "88px",
  });
});

$(window).on("scroll", function () {
  if (window.scrollY <= 300) {
    if (window.innerWidth > 991) {
      $(".landing .text").css({
        top: " 150px",
      });
    } else {
      $(".landing .text").css({
        top: " 100px",
      });
    }

    $(".landing-card ").css({
      left: "88px",
    });
  } else {
    $(".landing .text").css({
      top: " -15%",
    });
    $(".landing-card ").css({
      left: "-55%",
    });
  }
});

let scrollSec = document.querySelector(".home-cards");
let homeCard = document.querySelector(".home .card");
let offerScroll = document.querySelector(".first-row");
let offersCard = document.querySelector(".offers-card");
let offerScroll2 = document.querySelector(".second-row");
let cardSec = document.querySelector(".home-cards");
let text = document.querySelector(".land-sterling");
$(".home .right-arrow").click(function () {
  scrollSec.scrollBy(homeCard.clientWidth + 20, 0);
});
$(".home .left-arrow").click(function () {
  scrollSec.scrollBy(-homeCard.clientWidth - 20, 0);
});

$(".left-arrow1").click(function () {
  offerScroll.scrollBy(-offersCard.clientWidth - 20, 0);
});
$(".right-arrow2").click(function () {
  offerScroll.scrollBy(offersCard.clientWidth + 20, 0);
});

$(".left-arrow2").click(function () {
  offerScroll2.scrollBy(-offersCard.clientWidth - 20, 0);
});
$(".right-arrow1").click(function () {
  offerScroll2.scrollBy(offersCard.clientWidth + 20, 0);
});
$(window).on("scroll", function () {
  if (window.scrollY >= homeCard.offsetTop + homeCard.scrollHeight + 300) {
    $(".home .card").css({
      transform: "translatex(0)",
      opacity: 1,
    });
  } else {
    $(".home .card:first-of-type ").css({
      transform: "translateX(-60%)",
      opacity: 0,
    });
    $(".home .card:nth-of-type(2) ").css({
      transform: "translatey(-60%)",
      opacity: 0,
    });
    $(".home .card:nth-of-type(3) ").css({
      transform: "translateX(60%)",
      opacity: 0,
    });
  }
});
$(window).on("scroll", function () {
  if (
    window.scrollY >=
    offerScroll.offsetTop + offerScroll.scrollHeight + 500
  ) {
    $(".first-row .offers-card").css({
      transform: "translatex(0)",
      opacity: 1,
    });
  } else {
    $(".offers-card:first-of-type").css({
      transform: "translateX(-60%)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".offers-card:nth-of-type(2)").css({
      transform: "translatey(-60%)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".offers-card:nth-of-type(3)").css({
      transform: "translatex(60%)",
      opacity: 0,
      transition: "1.2s",
    });
  }
  if (window.scrollY >= offerScroll2.offsetTop + 1000) {
    $(".second-row .offers-card").css({
      transform: "translatex(0)",
      opacity: 1,
    });
  } else {
    $(".second-row .offers-card:first-of-type").css({
      transform: " translateX(-60%)",
      opacity: 0,
      transition: " 1.2s",
    });
    $(".second-row .offers-card:nth-of-type(2)").css({
      transform: " translatey(60%)",
      opacity: 0,
      transition: " 1.2s",
    });
    $(".second-row .offers-card:nth-of-type(3)").css({
      transform: " translateX(60%)",
      opacity: 0,
      transition: " 1.2s",
    });
  }
  if (window.scrollY >= text.offsetTop - 350) {
    $(".land-sterling .text h2 span").css({
      transform: "translate(0)",
      opacity: 1,
      transition: " 1s",
    });
  } else {
    $(".land-sterling .text h2 span:first-of-type ").css({
      transform: "translatey(-300px)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".land-sterling .text h2 span:nth-of-type(2) ").css({
      transform: "translatex(-300px)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".land-sterling .text h2 span:nth-of-type(3) ").css({
      transform: "translatex(300px)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".land-sterling .text h2 span:nth-of-type(4) ").css({
      transform: "translatex(-400px)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".land-sterling .text h2 span:nth-of-type(5) ").css({
      transform: "translatey(300px)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".land-sterling .text h2 span:nth-of-type(6) ").css({
      transform: "translatex(300px)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".land-sterling .text h2 span:nth-of-type(7) ").css({
      transform: "translatey(-300px)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".land-sterling .text h2 span:nth-of-type(8) ").css({
      transform: "translatey(-600px)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".land-sterling .text h2 span:nth-of-type(9) ").css({
      transform: "translatex(700px)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".land-sterling .text h2 span:nth-of-type(10) ").css({
      transform: "translatex(-300px)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".land-sterling .text h2 span:nth-of-type(11) ").css({
      transform: "translatey(-300px)",
      opacity: 0,
      transition: "1.2s",
    });
    $(".land-sterling .text h2 span:nth-of-type(12) ").css({
      transform: "translatex(400px)",
      opacity: 0,
      transition: "1.2s",
    });
  }
});

$("#register").click(function () {
   
    var element = document.getElementsByClassName("navbar-toggler")[0];
    
    element.classList.add("collapsed")
    element.setAttribute("aria-expanded", false);
    var element2 = document.getElementById("navbarSupportedContent");
    element2.classList.remove("show");
    
});


$("#sign-in").click(function () {
    var element = document.getElementsByClassName("navbar-toggler")[0];

    element.classList.add("collapsed")
    element.setAttribute("aria-expanded", false);
    var element2 = document.getElementById("navbarSupportedContent");
    element2.classList.remove("show");
});

