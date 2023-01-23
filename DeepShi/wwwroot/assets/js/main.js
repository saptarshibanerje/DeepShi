/**
* Template Name: WeBuild - v4.9.0
* Template URL: https://bootstrapmade.com/free-bootstrap-coming-soon-template-countdwon/
* Author: BootstrapMade.com
* License: https://bootstrapmade.com/license/
*/
(function () {
    "use strict";

    /**
     * Easy selector helper function
     */
    const select = (el, all = false) => {
        el = el.trim()
        if (all) {
            return [...document.querySelectorAll(el)]
        } else {
            return document.querySelector(el)
        }
    }

    /**
     * Countdown timer
     */

    let countdown = select('.countdown');

    const countDownDate = function () {
        let timeleft = new Date(countdown.getAttribute('data-count')).getTime() - new Date().getTime();

        let weeks = Math.floor(timeleft / (1000 * 60 * 60 * 24 * 7));
        let days = Math.floor(timeleft / (1000 * 60 * 60 * 24));
        let hours = Math.floor((timeleft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        let minutes = Math.floor((timeleft % (1000 * 60 * 60)) / (1000 * 60));
        let seconds = Math.floor((timeleft % (1000 * 60)) / 1000);

        let output = countdown.getAttribute('data-template');
        output = output.replace('%w', weeks.toString().padStart(2, '0')).replace('%d', days.toString().padStart(2, '0')).replace('%h', hours.toString().padStart(2, '0')).replace('%m', minutes.toString().padStart(2, '0')).replace('%s', seconds.toString().padStart(2, '0'));
        countdown.innerHTML = output;
    }
    countDownDate();
    setInterval(countDownDate, 1000);

})()