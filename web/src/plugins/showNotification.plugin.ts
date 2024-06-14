/*
 * @plugin Show Pagination
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-21
 *
 * This file contains the Show Pagination plugin to show messages
 */

// Register the plugin
export default defineNuxtPlugin((nuxtApp) => {

    // Register a function globally
    nuxtApp.provide('showNotification', (type: string, message: string) => {

        // Set class for colors
        const bgColor = type === 'success' ? 'vc-popup-notification-success' : 'vc-popup-notification-error';
    
        // Create popup
        const popup = `<div class="vc-popup-notification ${bgColor}">${message}</div>`;
    
        // Insert popup
        document.body.insertAdjacentHTML('beforeend', popup);
    
        // Wait 2.5 seconds and start to hide the message
        setTimeout(() => {
          (document.getElementsByClassName('vc-popup-notification')[0] as HTMLElement).style.opacity = '0';
        }, 2500);
    
        // Remove the message
        setTimeout(() => {
          document.getElementsByClassName('vc-popup-notification')[0].remove();
        }, 3000);

    });

});
