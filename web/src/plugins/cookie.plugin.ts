import { useCookie } from '#app';
import { useUserStore } from '~/stores/user';

export default defineNuxtPlugin((nuxtApp) => {
  nuxtApp.hook('vue:setup', () => {
    const userStore = useUserStore();
    const cookieToken = useCookie('jwtToken').value ?? '';
    if (cookieToken) {
      userStore.saveToken(cookieToken);
    }
  });
});
