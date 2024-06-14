/*
 * @middleware Routes
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-06-09
 *
 * This goal of this middleware is to set the layout by url segment
 */

// Logic
export default defineNuxtRouteMiddleware((to, from) => {
  if (to.path.startsWith('/auth/')) {
    setPageLayout('auth');
  } else if (to.path.startsWith('/admin/')) {
    setPageLayout('admin');
  } else {
    setPageLayout('default');
  }
});
