// System Utils
import { useRouter } from 'vue-router';

// Installed Utils
import { defineStore } from 'pinia';

// App Utils
import type ApiResponse from '~/interfaces/apiResponse';
import type { User } from '~/interfaces/user';
import type Settings from '~/interfaces/settings';
import { useUserStore } from './user';

// Settings Store
export const useSettingsStore = defineStore('settings', {
  state: () => ({
    options: null as Settings | null,
  }),
  getters: {
    optionsList: (state) => state.options,
  },
  actions: {
    async getOptions(path: string) {
      const config = useRuntimeConfig();
      const router = useRouter();
      try {
        const settings: ApiResponse<null> & {
          website: Settings;
          user: User;
        } = await $fetch(config.public.apiUrl + 'api/v1.0/settings', {});

        if (settings.success) {
          if (typeof settings.website !== 'undefined') {
            this.options = settings.website;
          }
          if (typeof settings.user !== 'undefined') {
            const userStore = useUserStore();
            userStore.saveUser(settings.user);
            if (path.startsWith('/auth/')) {
              if (settings.user.role < 1) {
                return router.push('/admin/dashboard');
              } else {
                return router.push('/user/dashboard');
              }
            }
          } else if (path.startsWith('/admin/') || path.startsWith('/user/')) {
            return router.push('/auth/signin');
          }
        } else {
          console.error(settings.message);
          throw new Error(settings.message);
        }
      } catch (error) {
        console.error(error);
        if (path.startsWith('/admin/') || path.startsWith('/user/'))
          return router.push('/auth/signin');
      }
    },
  },
});
