/*
 * @directive Page Load
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-29
 *
 * This file contains the directive to run animation when the pages are loading
 */

export const pageLoadDirective = {
  mounted(el: {
    classList: { add: (arg0: string) => void };
    getElementsByClassName: (arg0: string) => { textContent: string }[];
  }) {
    // Add overflow hide to the body
    document.body.style.overflow = 'hidden';

    // Default counter
    let c: number = 0;

    // Timer
    const timer = setInterval(() => {
      // Verify if the limit was reached
      if (c === 100) {
        // Wait for 700 milleseconds
        setTimeout(() => {
          // Add the vc-page-loading-hide class
          el.classList.add('vc-page-loading-hide');

          // Wait for 300 milleseconds
          setTimeout(() => {
            // Stop
            clearInterval(timer);

            // Remove style from the body
            document.body.style.overflow = '';
          }, 300);
        }, 700);
      } else {
        // Increase counter
        c = c + 1;

        // Display the percentage
        el.getElementsByClassName('vc-loading-text')[0].textContent = c + '%';
      }
    }, 10);
  },
};