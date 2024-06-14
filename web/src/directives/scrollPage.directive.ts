/*
 * @directive Scroll Page
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-29
 *
 * This file contains the directive to scroll the home page at menu click
 */

export const scrollPageDirective = {
  
  mounted(el: { addEventListener: (arg0: string, arg1: (event: MouseEvent) => void) => void; getAttribute: (arg0: string) => any; }) {

    // Event for menu links click in the top bar
    el.addEventListener('click', (event: MouseEvent) => {
      event.preventDefault()

      // Get the data id for the section id in the home page
      const dataId = el.getAttribute('data-id')

      // Scroll the page
      if (dataId === '#features') {
        const featuresEl = document.querySelector('.vc-features') as HTMLElement | null;
        if (featuresEl) {
          const featuresTopPosition = featuresEl.offsetTop;
          window.scrollTo({ top: featuresTopPosition, behavior: 'smooth' })
        }
      } else if (dataId === '#pricing') {
        const plansEl = document.querySelector('.vc-plans') as HTMLElement | null;
        if (plansEl) {
          const plansTopPosition = plansEl.offsetTop;
          window.scrollTo({ top: plansTopPosition, behavior: 'smooth' })
        }
      } else if (dataId === '#faq') {
        const faqEl = document.querySelector('.vc-faq') as HTMLElement | null;
        if (faqEl) {
          const faqTopPosition = faqEl.offsetTop;
          window.scrollTo({ top: faqTopPosition, behavior: 'smooth' })
        }
      }
    });
  }

};