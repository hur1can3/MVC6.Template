Navigation = {
    init: function () {
        var navigation = this;
        navigation.element = document.querySelector('.navigation');
        navigation.nodes = [].map.call(navigation.element.querySelectorAll('li'), function (node) {
            return node;
        });
        navigation.activeNodes = [].map.call(navigation.element.querySelectorAll('.has-active'), function (node) {
            return node;
        });

        if (navigation.element) {
            navigation.search = navigation.element.querySelector('input');

            navigation.search.addEventListener('input', function () {
                navigation.filter(this.value);
            });

            navigation.nodes.filter(function (node) {
                return node.classList.contains('submenu');
            }).forEach(function (submenu) {
                submenu.firstElementChild.addEventListener('click', function (e) {
                    e.preventDefault();

                    submenu.classList.toggle('open');

                    if (navigation.element.clientWidth < 100) {
                        [].forEach.call(submenu.parentElement.children, function (sibling) {
                            if (sibling != submenu) {
                                sibling.classList.remove('open');
                            }
                        });
                    }
                });
            });

            window.addEventListener('resize', function () {
                if (navigation.element.clientWidth < 100) {
                    navigation.closeAll();
                }
            });

            window.addEventListener('click', function (e) {
                if (navigation.element.clientWidth < 100) {
                    var target = e && e.target;

                    while (target && !/navigation/.test(target.className)) {
                        target = target.parentElement;
                    }

                    if (!target && target != window) {
                        navigation.closeAll();
                    }
                }
            });

            if (navigation.element.clientWidth < 100) {
                navigation.closeAll();
            }
        }
    },

    filter: function (term) {
        this.search.value = term;
        term = term.toLowerCase();

        this.nodes.forEach(function (node) {
            node.classList.remove('open');
            node.style.display = '';
        });

        if (term) {
            [].forEach.call(this.element.lastElementChild.children, function (node) {
                filterNode(node, term);
            });
        } else {
            this.activeNodes.forEach(function (node) {
                node.classList.add('open');
            });
        }

        function filterNode(element, term) {
            var text = element.firstElementChild.querySelector('.text').textContent.toLowerCase();
            var matches = text.indexOf(term) >= 0;

            if (element.classList.contains('submenu')) {
                var children = element.lastElementChild.children;

                for (var i = 0; i < children.length; i++) {
                    if (filterNode(children[i], term)) {
                        element.classList.add('open');
                    }
                }
            }

            if (!matches && !element.classList.contains('open')) {
                element.style.display = 'none';
            }

            return matches;
        }
    },

    closeAll: function () {
        this.nodes.forEach(function (node) {
            node.classList.remove('open');
        });
    }
};
