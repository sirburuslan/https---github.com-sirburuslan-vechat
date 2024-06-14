/*
 * @plugin Scroll Page
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-04-23
 *
 * This file contains the Scroll Page plugin to scroll the home page on menu click
 */

// App Utils
import { scrollPageDirective } from "~/directives/scrollPage.directive"

// Register the plugin
export default defineNuxtPlugin((nuxtApp) => {

  // Directive for scroll page
  nuxtApp.vueApp.directive('scrollPage', scrollPageDirective)

})
