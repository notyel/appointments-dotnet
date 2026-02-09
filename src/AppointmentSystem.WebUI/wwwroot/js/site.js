// Site wide JS
document.addEventListener('DOMContentLoaded', function() {
    console.log('Appointment System Loaded');

    // Example: Highlight active navigation link
    const currentPath = window.location.pathname;
    const navLinks = document.querySelectorAll('nav a');
    navLinks.forEach(link => {
        if (link.getAttribute('href') === currentPath) {
            link.style.borderBottom = '2px solid white';
        }
    });
});

// Booking specific functions
function confirmCancel() {
    return confirm('¿Está seguro de que desea cancelar esta cita?');
}

// Form Validation
document.querySelectorAll('form').forEach(form => {
    form.addEventListener('submit', function(event) {
        let isValid = true;
        const requiredFields = form.querySelectorAll('[required]');

        requiredFields.forEach(field => {
            if (!field.value.trim()) {
                isValid = false;
                field.style.borderColor = 'var(--error-color)';

                // Add error message if not exists
                let errorMsg = field.parentNode.querySelector('.error-message');
                if (!errorMsg) {
                    errorMsg = document.createElement('span');
                    errorMsg.className = 'error-message';
                    errorMsg.style.color = 'var(--error-color)';
                    errorMsg.style.fontSize = '0.8rem';
                    errorMsg.textContent = 'Este campo es requerido.';
                    field.parentNode.appendChild(errorMsg);
                }
            } else {
                field.style.borderColor = 'var(--border-color)';
                const errorMsg = field.parentNode.querySelector('.error-message');
                if (errorMsg) errorMsg.remove();
            }
        });

        if (!isValid) {
            event.preventDefault();
        }
    });
});
