/*
 * @middleware Settings
 *
 * @author Ruslan Sirbu
 * @version 0.0.1
 * @updated 2024-05-07
 *
 * This goal of this middleware is to load the website settings and user's data
 */

// Installed Utils
import { ofetch } from 'ofetch';

// Logic
export default defineNuxtRouteMiddleware((to, from) => {

  // Add token to the $fetch requests
  globalThis.$fetch = ofetch.create({
    onRequest: ({ options }) => {
      options.headers = {
        ...options.headers,
        Authorization: `Bearer ${useCookie('jwtToken').value ?? ''}`,
      };
    },
  });

  // Push router value
  const pushRouter = (isAuthenticated: boolean, role: number = 0) => {
    if (to.path.startsWith('/auth/')) {
      if (role > 0) return navigateTo('/admin/dashboard');
      else navigateTo('/user/dashboard');
    } else if (to.path.startsWith('/admin/')) {
      if (!isAuthenticated) {
        return navigateTo('/auth/signin');
      }
    }
  };

  const config = useRuntimeConfig();

  if (config.public.env !== 'test') {

    // Get all website settings store
    const websiteSettings = useSettingsStore();

    // Get user store
    const userStore = useUserStore();

    // Check if is requested sign out process
    if ( to.fullPath === '/auth/signout' ) {
      console.log('Good');
      userStore.deleteState();
      return navigateTo('/auth/signin');
    }

    // Verify if user is unauthenticated but the token is saved
    if (!websiteSettings.optionsList) {
      userStore.isLoading = true;
      websiteSettings.getOptions(to.path);
    } else if (userStore.user !== null) {
      return pushRouter(true, userStore.user.role);
    } else {
      return pushRouter(false);
    }
  }
});
