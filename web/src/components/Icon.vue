<script lang="ts" setup>

// Allowed properties
const props = defineProps({
  name: {
    type: String,
    required: true
  },
  extraClass: {
    type: String,
    required: ''
  }
})

// Get properties
const { name, extraClass } = props

</script>

<template>
  <span :class="['material-symbols-outlined', extraClass ? ' ' + extraClass : '']">
    {{ name }}
  </span>
</template>