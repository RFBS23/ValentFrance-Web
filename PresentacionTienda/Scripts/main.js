
/* swiper */
var swiperCategories = new Swiper(".categories__container", {
    
    spaceBetween: 24,
    loop: true,
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
    breakpoints: {
        640: {
            slidesPerView: 2,
            spaceBetween: 20,
        },
        768: {
            slidesPerView: 4,
            spaceBetween: 40,
        },
        1400: {
            slidesPerView: 6,
            spaceBetween: 24,
        },
    },
});

/**new arrivals*/
/* swiper */
var swiperProducts = new Swiper(".new__container", {
    spaceBetween: 24,
    loop: true,
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
    breakpoints: {
        640: {
            slidesPerView: 2,
            spaceBetween: 20,
        },
        768: {
            slidesPerView: 4,
            spaceBetween: 40,
        },
        1400: {
            slidesPerView: 4,
            spaceBetween: 24,
        },
    },
});

/*productos*/
const tabs = document.querySelectorAll('[data-target]'),
    tabContents = document.querySelectorAll('[content]');
tabs.forEach((tab) => {
    tab.addEventListener('click', () => {
        const target = document.querySelector(tab.dataset.target);
        tabContents.forEach((tabContent) => {
            tabContent.classList.remove('active-tab');
        });
        target.classList.add('active-tab');
        tabs.forEach((tab) => {
            tab.classList.remove('active-tab');
        });
        tab.classList.add('active-tab');
    });
});

/*galeria de imagenes*/
function imgGallery() {
    const mainImg = document.querySelector('.details__img'),
        smallImg = document.querySelectorAll('.details__small-img');

    smallImg.forEach((img) => {
        img.addEventListener('click', function () {
            mainImg.src = this.src;
        });
    });
}

imgGallery();