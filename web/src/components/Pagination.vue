<script lang="ts" setup>

// Allowed properties
const props = defineProps({
    scope: {
        type: String,
        required: true
    },
    page: {
        type: Number,
        required: true
    },
    limit: {
        type: Number,
        required: true
    },
    total: {
        type: Number,
        required: true
    }
});

// Get properties
const scope = toRef(props, 'scope');
const page = toRef(props, 'page');
const limit = toRef(props, 'limit');
const total = toRef(props, 'total');

// Count pages
const totalPages: number = Math.ceil(total.value / limit.value) + 1;

// Calculate start page
const from: number = page.value > 2 ? page.value - 2 : 1;

// Create an array with pages
const pages = computed<number[]>(() => {
    
  // Calculate the number of pages divided by limit
  const totalPages = Math.ceil(total.value / limit.value);

  // Calculate start and end page numbers
  let startPage = Math.max(page.value - 2, 1);
  let endPage = Math.min(startPage + 4, totalPages);

  // Adjust startPage if we're at the end of the page range
  if (endPage === totalPages) {
    startPage = Math.max(endPage - 4, 1);
  }

  // Generate the array of page numbers
  return Array.from({ length: (endPage - startPage) + 1 }, (_, i) => startPage + i);  
  
});

// Events Emiter
const navigate = defineEmits(['update:page']);

/**
 * Navigate to a page
 * 
 * @param number page
 */
const navigateTo = (page: number) => {
    navigate('update:page', page);
}

</script>
<template>
    <div class="vc-navigation flex justify-between items-center justify-center pl-3 pr-3 vc-transparent-color">
        <h3>
            {{ (((page - 1) * limit) + 1) + '-' + (((page * limit) < total)?(page * limit):total) }} {{ $t('of') }} {{ total}} {{ $t('results') }}
        </h3>
        <ul class="flex" :data-scope="scope">
            <li class="page-item" v-if="page > 1">
                <NuxtLink href="#" class="page-link" @click.prevent="navigateTo(page - 1)" external>
                    <Icon name="navigate_before" />
                </NuxtLink>
            </li>
            <li class="page-item" v-else>
                <NuxtLink href="#" class="page-link disabled" external>
                    <Icon name="navigate_before" />
                </NuxtLink>
            </li>            
            <li class="page-item" v-for="item in pages" :key="item">
                <NuxtLink href="#" class="page-link" :class="{'active': (item === page)}" @click.prevent="navigateTo(item)" external>
                    {{ item }}
                </NuxtLink>
            </li>
            <li class="page-item" v-if="(limit * page) < total">
                <NuxtLink href="#" class="page-link" @click.prevent="navigateTo(page + 1)" external>
                    <Icon name="navigate_next" />
                </NuxtLink>
            </li>
            <li class="page-item" v-else>
                <NuxtLink href="#" class="page-link disabled" external>
                    <Icon name="navigate_next" />
                </NuxtLink>
            </li>             
        </ul>
    </div>
</template>