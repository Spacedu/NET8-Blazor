// Fullscreen Logic
function setupFullscreen() {
    const fullscreenBtn = document.querySelector('[data-toggle="fullscreen"]');

    if (fullscreenBtn) {
        fullscreenBtn.addEventListener("click", function (e) {
            e.preventDefault();
            document.body.classList.toggle("fullscreen-enable");

            if (!document.fullscreenElement &&
                !document.mozFullScreenElement &&
                !document.webkitFullscreenElement) {
                // Enter fullscreen
                if (document.documentElement.requestFullscreen) {
                    document.documentElement.requestFullscreen();
                } else if (document.documentElement.mozRequestFullScreen) {
                    document.documentElement.mozRequestFullScreen();
                } else if (document.documentElement.webkitRequestFullscreen) {
                    document.documentElement.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
                }
            } else {
                // Exit fullscreen
                if (document.cancelFullScreen) {
                    document.cancelFullScreen();
                } else if (document.mozCancelFullScreen) {
                    document.mozCancelFullScreen();
                } else if (document.webkitCancelFullScreen) {
                    document.webkitCancelFullScreen();
                }
            }
        });
    }

    // Monitor fullscreen change events
    document.addEventListener("fullscreenchange", handleFullscreenChange);
    document.addEventListener("webkitfullscreenchange", handleFullscreenChange);
    document.addEventListener("mozfullscreenchange", handleFullscreenChange);
}

function handleFullscreenChange() {
    if (!document.webkitIsFullScreen &&
        !document.mozFullScreen &&
        !document.msFullscreenElement) {
        document.body.classList.remove("fullscreen-enable");
    }
}

// Light/Dark Mode Logic
function setupLightDarkMode() {
    const html = document.getElementsByTagName("HTML")[0];
    const lightDarkModeButtons = document.querySelectorAll(".light-dark-mode");

    if (lightDarkModeButtons && lightDarkModeButtons.length) {
        lightDarkModeButtons[0].addEventListener("click", function (e) {
            if (html.hasAttribute("data-bs-theme") &&
                html.getAttribute("data-bs-theme") == "dark") {
                // Switch to light mode
                changeAttribute("data-bs-theme", "light", "layout-mode-light", html);
            } else {
                // Switch to dark mode
                changeAttribute("data-bs-theme", "dark", "layout-mode-dark", html);
            }

            // Dispatch resize event to handle any visual updates needed
            window.dispatchEvent(new Event('resize'));
        });
    }
}

// Helper function for changing attributes used by the light/dark toggle
function changeAttribute(attribute, value, targetId, element) {
    const targetElement = document.getElementById(targetId);
    element.setAttribute(attribute, value);
    if (targetElement) {
        document.getElementById(targetId).click();
    }
}


// Initialize both features
    setupFullscreen();
    setupLightDarkMode();
