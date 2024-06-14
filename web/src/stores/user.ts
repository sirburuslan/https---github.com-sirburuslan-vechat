// Installed Utils
import { defineStore } from 'pinia';

// App Utils
import type { User } from '~/interfaces/user';
import { sanitizeInput } from '~/utils/sanitize';

// User Store
export const useUserStore = defineStore('user', {
  state: () => ({
    user: null as User | null,
    token: null as string | null,
    isAuthenticated: false,
    isLoading: false,
  }),
  getters: {
    sidebarStatus: (state) =>
      state.user !== null ? state.user.sidebarStatus : 0,
  },
  actions: {
    saveUser(data: User) {
      if (data) {
        this.$state.user = {
          userId: (typeof data.userId !== 'number')?parseInt(sanitizeInput(data.userId)):data.userId,
          firstName: sanitizeInput(data.firstName),
          lastName: sanitizeInput(data.lastName),
          email: sanitizeInput(data.email),
          role: typeof data.role === 'number' ? data.role : 1,
          sidebarStatus:
            typeof data.sidebarStatus === 'string'
              ? parseInt(data.sidebarStatus)
              : 0,
        };
        if (data.token) {
          const token = useCookie('jwtToken');
          token.value = sanitizeInput(data.token);
        }
        this.$state.isAuthenticated = true;
        this.$state.isLoading = false;
        /*const token = useCookie('jwtToken');
        token.value = sanitizeInput(data.token);
        this.$patch({
          user: {
            userId: sanitizeInput(data.userId),
            firstName: sanitizeInput(data.firstName),
            lastName: sanitizeInput(data.lastName),
            email: sanitizeInput(data.email),
            role: (typeof data.role === 'number')?data.role:1,
            sidebarStatus: (typeof data.sidebarStatus === 'string')?parseInt(data.sidebarStatus):0
          },
          token: this.$state.token,
          isAuthenticated: true,
          isLoading: false
        });   */
      }
    },
    saveToken(token: string) {
      this.$state.token = token;
    },
    updateSidebarStatus(status: number) {
      if (this.user) this.user.sidebarStatus = status;
      else
        this.$patch({
          user: {
            sidebarStatus: status,
          },
          isAuthenticated: false,
          isLoading: false,
        });
    },
    deleteState() {
      const jwtToken = useCookie('jwtToken');
      jwtToken.value = null;
      this.$patch({
        user: null,
        token: null,
        isAuthenticated: false,
        isLoading: false,
      });
    },
  },
});
