<template>
  <PageLoading />
  <main class="vc-auth-main">
    <div class="container mx-auto px-4 md:px-6 vc-auth-container">
      <div class="max-w-md mx-auto">
          <!--{(websiteOptions.SignInPageLogo)?(
              <div class="grid">
                  <div class="col-1 text-center">
                      <Link href={'/'} class="vc-auth-logo">
                          <Image src={websiteOptions.SignInPageLogo} fill={true} sizes="(max-width: 200px)" alt="User Access" />
                      </Link>
                  </div>
              </div>
          ):''}-->
          <div class="grid">
              <div class="col-1">
                <slot />
              </div>
          </div>
      </div>
    </div>
  </main>
</template>

<style lang="scss">
// Styles for auth area
@import '@/assets/styles/auth/_main.scss';
</style>