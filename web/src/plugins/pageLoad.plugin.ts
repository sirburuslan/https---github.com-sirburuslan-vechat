/*
 * @plugin Page Load
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-29
 *
 * This file contains the Page Load plugin to run animation when the pages are loading
 */

// App Utils
import { pageLoadDirective } from "~/directives/pageLoad.directive";

// Register the plugin
export default defineNuxtPlugin((nuxtApp) => {
  // Directive for page load
  nuxtApp.vueApp.directive('pageLoad', pageLoadDirective);
});
