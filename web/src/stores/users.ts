// Installed Utils
import { defineStore } from 'pinia';

// App Utils
import type ApiResponse from '~/interfaces/apiResponse';
import type ElementsResponse from '~/interfaces/elementsResponse';
import type { CreateUser, User } from '~/interfaces/user';

// Users Store
export const useUsersStore = defineStore('users', {
    state: () => ({
        users: null as User[] | null,
        page: 1,
        limit: 24,
        total: 0,
        search: '',
        time: 0
    }),
    actions: {

        async createUser(user: CreateUser) {
        
            // Get the runntime configuration
            const configuration = useRuntimeConfig();
        
            try {
        
                // Create a new user
                const response: ApiResponse<null> = await $fetch(
                configuration.public.apiUrl + 'api/v1.0/admin/users/create',
                {
                    method: 'POST',
                    headers: {
                    'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(user),
                }
                );

                // Check if the response is success
                if (response.success) {
                    await this.getUsers(this.page, this.search);
                }
                
                return response;
        
            } catch (error: unknown) {
                
                // Set error
                return error instanceof Error ? error.message : '';
        
            }
    
        },

        async getUsers(page: number, search: string) {

            // Get the configuration
            const config = useRuntimeConfig();

            // Empty the users list
            this.users = null;

            // Set page number
            this.page = page;

            // Reset total
            this.total = 0;

            // Set search
            this.search = search;

            try {

                // Get the users
                const users: ElementsResponse<User[]> = await $fetch(config.public.apiUrl + `api/v1.0/admin/users/list?page=${page}&search=${search}`);
                
                // Verify if users exists
                if ( users.success ) {

                    // Update the users list
                    this.users = users.result.elements;

                    // Update total number of elements
                    this.total = users.result.total;

                    // Update the server time
                    this.time = users.time;

                }

            } catch ( error ) {
                console.error(error);
            }

        },

        setCurrentPage(page: number) {
            this.page = page;
        }      

    }

});