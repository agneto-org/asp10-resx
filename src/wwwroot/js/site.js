// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Multi-Language App JavaScript

(function () {
    'use strict';

    // Add fade-in animation to main content on page load
    document.addEventListener('DOMContentLoaded', function () {
        var mainContent = document.querySelector('main');
        if (mainContent) {
            mainContent.classList.add('fade-in');
        }

        // Initialize feature cards hover effect
        initFeatureCards();

        // Initialize language badge animation
        initLanguageBadge();
    });

    // Feature cards initialization
    function initFeatureCards() {
        var cards = document.querySelectorAll('.feature-card');
        cards.forEach(function (card) {
            card.addEventListener('mouseenter', function () {
                this.style.cursor = 'pointer';
            });
        });
    }

    // Language badge initialization
    function initLanguageBadge() {
        var badge = document.querySelector('.language-badge');
        if (badge) {
            badge.addEventListener('click', function () {
                var dropdown = document.querySelector('.language-dropdown');
                if (dropdown) {
                    dropdown.classList.toggle('show');
                }
            });
        }
    }

    // Utility function to check if static assets are loaded
    window.checkStaticAssets = function () {
        return {
            cssLoaded: document.styleSheets.length > 0,
            jsLoaded: true,
            timestamp: new Date().toISOString()
        };
    };
})();
