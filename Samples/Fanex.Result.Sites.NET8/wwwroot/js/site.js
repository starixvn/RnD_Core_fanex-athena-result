// Basic JavaScript for the upgraded .NET 8 application

// Global variables
window.rootUrl = window.rootUrl || '/';

// Utility functions
function showLoading() {
    const loadingElement = document.getElementById('loadingMsgBox');
    if (loadingElement) {
        loadingElement.classList.remove('hidden');
    }
}

function hideLoading() {
    const loadingElement = document.getElementById('loadingMsgBox');
    if (loadingElement) {
        loadingElement.classList.add('hidden');
    }
}

// AJAX helper function for jQuery compatibility
function makeRequest(url, options = {}) {
    if (typeof $ !== 'undefined') {
        // Use jQuery if available
        return $.ajax({
            url: url,
            type: options.method || 'GET',
            data: options.data,
            dataType: 'json',
            success: options.success,
            error: options.error
        });
    } else {
        // Fallback to fetch
        const defaultOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        };

        const requestOptions = { ...defaultOptions, ...options };

        return fetch(url, requestOptions)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            });
    }
}

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', function() {
    console.log('Fanex Result Core .NET 8 application loaded');
    
    // Add any initialization code here
    hideLoading();
});

// Export functions for global use
window.FanexResult = {
    showLoading,
    hideLoading,
    makeRequest
};
