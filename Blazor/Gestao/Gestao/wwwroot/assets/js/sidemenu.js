// IIFE (Immediately Invoked Function Expression) para evitar poluição do escopo global
(function () {
    // Referência para o HTML original do menu (não declarada globalmente)
    let originalNavbarMenu = null;

    // Captura o HTML original do menu quando o documento for carregado
    function captureOriginalMenu() {
        const navbarMenuElement = document.querySelector(".navbar-menu");
        if (navbarMenuElement) {
            originalNavbarMenu = navbarMenuElement.innerHTML;
        }
    }

    // Configuração inicial do menu lateral
    function initSidebar() {
        captureOriginalMenu();
        setupMenuCollapse();
        updateActiveMenuItems();
        setupMenuVisibility();
        setupTwoColumnMenu();
        setupVerticalHover();
        scrollToActiveMenuPosition();
    }

    // Configura os colapsos/expansões do menu
    function setupMenuCollapse() {
        const collapseMenuItems = document.querySelectorAll(".navbar-nav .collapse");

        if (!collapseMenuItems) return;

        Array.from(collapseMenuItems).forEach(function (item) {
            const collapseInstance = new bootstrap.Collapse(item, {
                toggle: false
            });

            // Evento quando um submenu é expandido
            item.addEventListener("show.bs.collapse", function (event) {
                event.stopPropagation();

                // Verifica se o item está dentro de outro collapse
                const parentCollapse = item.parentElement.closest(".collapse");

                if (parentCollapse) {
                    // Fecha outros submenus dentro do mesmo pai
                    const siblingCollapses = parentCollapse.querySelectorAll(".collapse");
                    Array.from(siblingCollapses).forEach(function (siblingCollapse) {
                        const siblingInstance = bootstrap.Collapse.getInstance(siblingCollapse);
                        if (siblingInstance !== collapseInstance) {
                            siblingInstance.hide();
                        }
                    });
                } else {
                    // Fecha outros menus do mesmo nível
                    const siblings = getSiblings(item.parentElement);
                    Array.from(siblings).forEach(function (sibling) {
                        if (sibling.childNodes.length > 2) {
                            sibling.firstElementChild.setAttribute("aria-expanded", "false");
                        }

                        const siblingCollapses = sibling.querySelectorAll("*[id]");
                        Array.from(siblingCollapses).forEach(function (collapse) {
                            collapse.classList.remove("show");

                            if (collapse.childNodes.length > 2) {
                                const links = collapse.querySelectorAll("ul li a");
                                Array.from(links).forEach(function (link) {
                                    if (link.hasAttribute("aria-expanded")) {
                                        link.setAttribute("aria-expanded", "false");
                                    }
                                });
                            }
                        });
                    });
                }
            });

            // Evento quando um submenu é fechado
            item.addEventListener("hide.bs.collapse", function (event) {
                event.stopPropagation();

                // Fecha todos os submenus internos
                const childCollapses = item.querySelectorAll(".collapse");
                Array.from(childCollapses).forEach(function (childCollapse) {
                    const childInstance = bootstrap.Collapse.getInstance(childCollapse);
                    childInstance.hide();
                });
            });
        });
    }

    // Retorna os elementos irmãos de um elemento
    function getSiblings(element) {
        const siblings = [];
        let sibling = element.parentNode.firstChild;

        while (sibling) {
            if (sibling.nodeType === 1 && sibling !== element) {
                siblings.push(sibling);
            }
            sibling = sibling.nextSibling;
        }

        return siblings;
    }

    // Atualiza o estado ativo dos itens de menu com base na URL atual
    function updateActiveMenuItems() {
        const currentPath = location.pathname === "/" ? "index.html" : location.pathname.substring(1);
        const currentFile = currentPath.substring(currentPath.lastIndexOf("/") + 1);

        if (!currentFile) return;

        // Layout vertical
        if (document.documentElement.getAttribute("data-layout") === "vertical") {
            activateVerticalMenuItems(currentFile);
        }
        // Layout de duas colunas
        else if (document.documentElement.getAttribute("data-layout") === "twocolumn") {
            activateTwoColumnMenuItems(currentFile);
        }
        // Layout horizontal
        else if (document.documentElement.getAttribute("data-layout") === "horizontal") {
            activateHorizontalMenuItems(currentFile);
        }
    }

    // Ativa itens de menu para layout vertical
    function activateVerticalMenuItems(currentFile) {
        const navbarNav = document.getElementById("navbar-nav");
        if (!navbarNav) return;

        const activeLink = navbarNav.querySelector(`[href="${currentFile}"]`);

        if (!activeLink) return;

        activeLink.classList.add("active");

        const parentCollapses = getParentCollapses(activeLink);

        // Expande todos os menus pai
        parentCollapses.forEach(collapse => {
            collapse.classList.add("show");
            if (collapse.parentElement.children[0]) {
                collapse.parentElement.children[0].classList.add("active");
                collapse.parentElement.children[0].setAttribute("aria-expanded", "true");

                // Verifica se há mais níveis superiores
                if (collapse.parentElement.closest(".collapse.menu-dropdown")) {
                    collapse.parentElement.closest(".collapse").classList.add("show");
                    if (collapse.parentElement.closest(".collapse").previousElementSibling) {
                        collapse.parentElement.closest(".collapse").previousElementSibling.classList.add("active");

                        // Para layout horizontal, verifica mais um nível
                        if (document.documentElement.getAttribute("data-layout") === "horizontal") {
                            if (collapse.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.closest(".collapse")) {
                                collapse.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.closest(".collapse").previousElementSibling.classList.add("active");
                            }
                        }
                    }
                }
            }
        });
    }

    // Busca todos os menus collapse pais de um elemento
    function getParentCollapses(element) {
        const collapses = [];
        let parent = element.closest(".collapse.menu-dropdown");

        while (parent) {
            collapses.push(parent);
            parent = parent.parentElement.closest(".collapse.menu-dropdown");
        }

        return collapses;
    }

    // Ativa itens do menu para layout de duas colunas
    function activateTwoColumnMenuItems(currentFile) {
        // Reset de classe no corpo
        if (document.body.className === "twocolumn-panel") {
            document.body.classList.remove("twocolumn-panel");
        }

        const navbarNav = document.getElementById("navbar-nav");
        if (!navbarNav) return;

        // Ativa o link no menu principal
        const activeMenuLink = navbarNav.querySelector(`[href="${currentFile}"]`);

        if (activeMenuLink) {
            activeMenuLink.classList.add("active");

            const parentCollapseDiv = activeMenuLink.closest(".collapse.menu-dropdown");

            if (parentCollapseDiv) {
                // Adiciona classe active
                parentCollapseDiv.classList.add("show");

                const parentCollapseLink = parentCollapseDiv.parentElement.children[0];
                if (parentCollapseLink) {
                    parentCollapseLink.classList.add("active");

                    // Se há mais níveis superiores
                    if (parentCollapseLink.parentElement.closest(".collapse.menu-dropdown")) {
                        parentCollapseLink.parentElement.closest(".collapse.menu-dropdown").parentElement.classList.add("twocolumn-item-show");

                        // Subir mais um nível, se necessário
                        if (parentCollapseLink.parentElement.parentElement.parentElement.parentElement.closest(".collapse.menu-dropdown")) {
                            const menuId = parentCollapseLink.parentElement.parentElement.parentElement.parentElement.closest(".collapse.menu-dropdown").getAttribute("id");

                            parentCollapseLink.parentElement.parentElement.parentElement.parentElement.closest(".collapse.menu-dropdown").parentElement.classList.add("twocolumn-item-show");

                            parentCollapseLink.parentElement.closest(".collapse.menu-dropdown").parentElement.classList.remove("twocolumn-item-show");

                            // Ativa o item na barra lateral de ícones
                            const twoColumnMenu = document.getElementById("two-column-menu");
                            if (twoColumnMenu && twoColumnMenu.querySelector(`[href="#${menuId}"]`)) {
                                twoColumnMenu.querySelector(`[href="#${menuId}"]`).classList.add("active");
                            }
                        }

                        // Ativa o item no menu de duas colunas
                        const menuId = parentCollapseDiv.getAttribute("id");
                        const twoColumnMenu = document.getElementById("two-column-menu");
                        if (twoColumnMenu && twoColumnMenu.querySelector(`[href="#${menuId}"]`)) {
                            twoColumnMenu.querySelector(`[href="#${menuId}"]`).classList.add("active");
                        }
                    }
                }
            }
        } else {
            // Se não encontrou link ativo, adiciona a classe twocolumn-panel
            document.body.classList.add("twocolumn-panel");
        }
    }

    // Ativa itens do menu para layout horizontal
    function activateHorizontalMenuItems(currentFile) {
        // Similar ao layout vertical, mas com manipulação específica para horizontal
        const navbarNav = document.getElementById("navbar-nav");
        if (!navbarNav) return;

        const activeLink = navbarNav.querySelector(`[href="${currentFile}"]`);

        if (!activeLink) return;

        activeLink.classList.add("active");

        const parentCollapses = getParentCollapses(activeLink);

        // Expande todos os menus pai
        parentCollapses.forEach(collapse => {
            collapse.classList.add("show");
            if (collapse.parentElement.children[0]) {
                collapse.parentElement.children[0].classList.add("active");
            }
        });
    }

    // Verifica se um elemento está visível na viewport
    function isInViewport(element) {
        if (!element) return false;

        let top = element.offsetTop;
        let left = element.offsetLeft;
        const width = element.offsetWidth;
        const height = element.offsetHeight;

        while (element.offsetParent) {
            element = element.offsetParent;
            top += element.offsetTop;
            left += element.offsetLeft;
        }

        return (
            top >= window.pageYOffset &&
            left >= window.pageXOffset &&
            (top + height) <= (window.pageYOffset + window.innerHeight) &&
            (left + width) <= (window.pageXOffset + window.innerWidth)
        );
    }

    // Configura a visibilidade do menu
    function setupMenuVisibility() {
        // Monitora cliques no ícone hamburger para mostrar/esconder o menu
        const hamburgerIcon = document.getElementById("topnav-hamburger-icon");

        if (hamburgerIcon) {
            hamburgerIcon.addEventListener("click", toggleMenuVisibility);
        }

        // Configura overlay para fechar menu em dispositivos móveis
        const verticalOverlays = document.getElementsByClassName("vertical-overlay");

        if (verticalOverlays) {
            Array.from(verticalOverlays).forEach(function (overlay) {
                overlay.addEventListener("click", function () {
                    document.body.classList.remove("vertical-sidebar-enable");

                    // Para layout de duas colunas
                    if (sessionStorage.getItem("data-layout") === "twocolumn") {
                        document.body.classList.add("twocolumn-panel");
                    } else {
                        // Outros layouts
                        document.documentElement.setAttribute("data-sidebar-size",
                            sessionStorage.getItem("data-sidebar-size"));
                    }
                });
            });
        }
    }

    // Alterna a visibilidade do menu
    function toggleMenuVisibility() {
        const windowWidth = document.documentElement.clientWidth;

        // Alterna o estado do ícone hamburger
        if (windowWidth > 767) {
            const hamburgerIcon = document.querySelector(".hamburger-icon");
            if (hamburgerIcon) {
                hamburgerIcon.classList.toggle("open");
            }
        }

        // Comportamento específico para cada layout
        if (document.documentElement.getAttribute("data-layout") === "horizontal") {
            document.body.classList.contains("menu")
                ? document.body.classList.remove("menu")
                : document.body.classList.add("menu");
        }
        // Layout vertical
        else if (document.documentElement.getAttribute("data-layout") === "vertical") {
            if (windowWidth <= 1025 && windowWidth > 767) {
                // Tamanho de tela médio
                document.body.classList.remove("vertical-sidebar-enable");

                document.documentElement.getAttribute("data-sidebar-size") === "sm"
                    ? document.documentElement.setAttribute("data-sidebar-size", "")
                    : document.documentElement.setAttribute("data-sidebar-size", "sm");
            } else if (windowWidth > 1025) {
                // Tela grande
                document.body.classList.remove("vertical-sidebar-enable");

                document.documentElement.getAttribute("data-sidebar-size") === "lg"
                    ? document.documentElement.setAttribute("data-sidebar-size", "sm")
                    : document.documentElement.setAttribute("data-sidebar-size", "lg");
            } else if (windowWidth <= 767) {
                // Tela pequena (mobile)
                document.body.classList.add("vertical-sidebar-enable");
                document.documentElement.setAttribute("data-sidebar-size", "lg");
            }
        }
        // Layout semibox
        else if (document.documentElement.getAttribute("data-layout") === "semibox") {
            if (windowWidth > 767) {
                // ... lógica para semibox
            } else {
                // Mobile
                document.body.classList.add("vertical-sidebar-enable");
                document.documentElement.setAttribute("data-sidebar-size", "lg");
            }
        }
        // Layout de duas colunas
        else if (document.documentElement.getAttribute("data-layout") === "twocolumn") {
            document.body.classList.contains("twocolumn-panel")
                ? document.body.classList.remove("twocolumn-panel")
                : document.body.classList.add("twocolumn-panel");
        }
    }

    // Configura o menu de duas colunas
    function setupTwoColumnMenu() {
        // Se o layout não for de duas colunas, retorna
        if (document.documentElement.getAttribute("data-layout") !== "twocolumn") return;

        // Cria a visão de ícones para duas colunas
        const navbarMenu = document.querySelector(".navbar-menu");
        const navbarNav = document.getElementById("navbar-nav");
        if (!navbarMenu || !navbarNav || !originalNavbarMenu) return;

        const iconViewList = document.createElement("ul");

        // Adiciona o logo ao menu de ícones
        iconViewList.innerHTML = '<a href="#" class="logo"><img src="assets/images/logo-sm.png" alt="" height="22"></a>';
        iconViewList.className = "twocolumn-iconview";

        // Cria itens de ícones a partir do menu principal
        Array.from(navbarNav.querySelectorAll(".menu-link")).forEach(function (link) {
            const iconItem = document.createElement("li");

            // Esconde os textos dos spans para mostrar apenas ícones
            link.querySelectorAll("span").forEach(function (span) {
                span.classList.add("d-none");
            });

            // Marca o item ativo se necessário
            if (link.parentElement.classList.contains("twocolumn-item-show")) {
                link.classList.add("active");
            }

            // Adiciona o link ao item
            iconItem.appendChild(link.cloneNode(true));
            iconViewList.appendChild(iconItem);

            // Ajusta as classes do link para a visão de ícones
            const linkInIconView = iconItem.querySelector(link.tagName);
            if (linkInIconView.classList.contains("nav-link")) {
                linkInIconView.classList.replace("nav-link", "nav-icon");
            }
            linkInIconView.classList.remove("collapsed", "menu-link");
        });

        // Insere o menu de ícones no DOM
        const twoColumnMenu = document.getElementById("two-column-menu");
        if (twoColumnMenu) {
            twoColumnMenu.innerHTML = iconViewList.outerHTML;

            // Configura o menu de ícones para responder aos cliques
            const twoColumnMenuLinks = twoColumnMenu.querySelector("ul").querySelectorAll("li a");
            if (twoColumnMenuLinks) {
                Array.from(twoColumnMenuLinks).forEach(function (link) {
                    const currentPath = location.pathname === "/" ? "index.html" : location.pathname.substring(1);
                    const currentFile = currentPath.substring(currentPath.lastIndexOf("/") + 1);

                    // Configura eventos de clique para os ícones
                    link.addEventListener("click", function (e) {
                        // Verifica se a página atual é diferente do link clicado ou se tem dropdown
                        if ((currentFile !== "/" + link.getAttribute("href")) || link.getAttribute("data-bs-toggle")) {
                            // Remove a classe do painel de duas colunas se estiver presente
                            if (document.body.classList.contains("twocolumn-panel")) {
                                document.body.classList.remove("twocolumn-panel");
                            }
                        }

                        // Esconde o menu de navegação em dois-colunas
                        if (navbarNav) {
                            navbarNav.classList.remove("twocolumn-nav-hide");
                        }

                        // Fecha o ícone hamburger
                        const hamburgerIcon = document.querySelector(".hamburger-icon");
                        if (hamburgerIcon) {
                            hamburgerIcon.classList.remove("open");
                        }

                        // Alterna a classe active no ícone clicado
                        if ((e.target && e.target.matches("a.nav-icon")) || (e.target && e.target.matches("i"))) {
                            // Remove active de qualquer outro ícone
                            const activeIcon = twoColumnMenu.querySelector("ul .nav-icon.active");
                            if (activeIcon !== null) {
                                activeIcon.classList.remove("active");
                            }

                            // Adiciona active ao ícone clicado
                            (e.target.matches("i") ? e.target.closest("a") : e.target).classList.add("active");

                            // Remove a classe twocolumn-item-show de qualquer item
                            const twoColItems = document.getElementsByClassName("twocolumn-item-show");
                            if (twoColItems.length > 0) {
                                twoColItems[0].classList.remove("twocolumn-item-show");
                            }

                            // Mostra o submenu correspondente
                            const targetElemId = (e.target.matches("i") ? e.target.closest("a") : e.target).getAttribute("href").slice(1);
                            if (document.getElementById(targetElemId)) {
                                document.getElementById(targetElemId).parentElement.classList.add("twocolumn-item-show");
                            }
                        }
                    });

                    // Se o link atual corresponde à página atual, marca como ativo
                    if (currentFile !== "/" + link.getAttribute("href") || link.getAttribute("data-bs-toggle")) {
                        // Não marcar como ativo
                    } else {
                        // Marcar como ativo e ajustar visibilidade
                        link.classList.add("active");
                        if (navbarNav) {
                            navbarNav.classList.add("twocolumn-nav-hide");
                        }

                        const hamburgerIcon = document.querySelector(".hamburger-icon");
                        if (hamburgerIcon) {
                            hamburgerIcon.classList.add("open");
                        }
                    }
                });
            }
        }

        // Adiciona SimpleBar para scrolling se necessário
        if (document.documentElement.getAttribute("data-layout") !== "horizontal") {
            if (navbarNav) {
                try {
                    new SimpleBar(navbarNav);
                } catch (error) {
                    console.log("Error initializing SimpleBar on navbar-nav:", error);
                }
            }

            const twocolumnIconview = document.getElementsByClassName("twocolumn-iconview")[0];
            if (twocolumnIconview) {
                try {
                    new SimpleBar(twocolumnIconview);
                } catch (error) {
                    console.log("Error initializing SimpleBar on twocolumn-iconview:", error);
                }
            }
        }
    }

    // Configura o comportamento de hover para o menu vertical
    function setupVerticalHover() {
        const verticalHoverBtn = document.getElementById("vertical-hover");
        if (!verticalHoverBtn) return;

        verticalHoverBtn.addEventListener("click", function () {
            if (document.documentElement.getAttribute("data-sidebar-size") === "sm-hover") {
                document.documentElement.setAttribute("data-sidebar-size", "sm-hover-active");
            } else {
                document.documentElement.setAttribute("data-sidebar-size", "sm-hover");
            }
        });
    }

    // Rola para a posição do item de menu ativo
    function scrollToActiveMenuPosition() {
        setTimeout(function () {
            const activeElement = document.querySelector("#navbar-nav .nav-item .active");

            if (!activeElement) return;

            const offsetTop = activeElement ? activeElement.offsetTop : 0;

            if (offsetTop > 300) {
                const menuContainer = document.getElementsByClassName("app-menu")[0];

                if (!menuContainer) return;

                const scrollableContainer = menuContainer.querySelector(".simplebar-content-wrapper");

                if (scrollableContainer) {
                    setTimeout(function () {
                        scrollableContainer.scrollTop = offsetTop === 330 ? offsetTop + 85 : offsetTop;
                    }, 0);
                }
            }
        }, 250);
    }

    // Atualiza SimpleBar conforme necessário
    function updateSimplebar() {
        // Redefine ou cria SimpleBar conforme necessário
        if (document.documentElement.getAttribute("data-layout") === "vertical" ||
            document.documentElement.getAttribute("data-layout") === "semibox") {
            // Lógica para restaurar o menu vertical
        }
    }

    // Inicializa todas as funções do menu
    let timeout;/*
    document.addEventListener("DOMContentLoaded", function () {
        

        // Responde a eventos de redimensionamento
        window.addEventListener("resize", function () {
            if (timeout) {
                clearTimeout(timeout);
            }
            timeout = setTimeout(function () {
                // Atualiza o menu com base no tamanho da janela
                if (document.documentElement.getAttribute("data-layout") !== "horizontal") {
                    updateSimplebar();
                }
            }, 2000);
        });
        alert("x");
    });
    */
    // Expõe funções ao escopo global se necessário
    window.sidebarMenu = {
        updateActiveMenuItems: updateActiveMenuItems,
        toggleMenuVisibility: toggleMenuVisibility
    };
    initSidebar();
})();

