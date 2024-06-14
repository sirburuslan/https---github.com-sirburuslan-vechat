<script setup lang="ts">
// System Utils
import { useHead } from '#imports'

// Define page name
useHead({
  title: process.env.SITE_NAME
})

</script>
<template>
  <TopBar />
  <Presentation />
  <Stats />
  <Features />
  <Plans />
  <Faq />
  <Footer />
</template>
